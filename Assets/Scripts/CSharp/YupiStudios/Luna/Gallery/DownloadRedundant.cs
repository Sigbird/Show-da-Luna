using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using MiniJSON;
using UnityEngine.UI;

namespace YupiPlay {		
	public class DownloadRedundant : MonoBehaviour {
		private enum States {
			READY, READING
		}	

		[Serializable]
		private struct Server {
			public string site;
			public int priority;

			public Server(string url, int mPriority) {			
				site = url;
				priority = mPriority;
			}
		}

		private Server[] localServers = new Server[] {
			new Server("https://yupiplay.blob.core.windows.net/luna/", 1),
            new Server("https://s3.amazonaws.com/yupiplay-luna/videos/", 1),
            new Server("https://yupiplay.000webhostapp.com/luna/", 2)
		};

		private const string FILENAME = "lunaservers.json";
		private const string FILEURL = "https://yupiplay.000webhostapp.com/luna/";
		private const string SERVERSKEY = "lunaservers";
		private const string LASTSERVERCHECK = "lunaserversday";

		private LinkedList<Server> servers;
		private States state = States.READING;
		private LinkedListNode<Server> lastServer;	
		private int currentPriority = 99;
		private int minPriority = 99;
		private int maxPriority = 1;
		private bool hasRunInit;
		private static DownloadRedundant instance;

		public static DownloadRedundant Instance {
			get {
				return instance;
			}
			private set {
				if (instance == null) {
					instance = value;
				}
			}
		}

        void Awake() {
            Instance = this;
        }

        void Start() {
            StartCoroutine(init());
        }        		

		private IEnumerator init() {			      
			if (canReadFromNetwork()) {
				readFromNetwork();
				yield break;
			}
			readFromPlayerPrefs();
		}

		public int GetCurrentPriority() {
			if (state == States.READY) {
				return currentPriority;	
			}
			return 0;
		}

        public int GetCurrentPriorityCount() {
            if (state == States.READY) {
                int count = 0;
                foreach (Server s in servers) {
                    if (s.priority == currentPriority) {
                        count++;
                    }
                }

                return count; 
            }
            return 0;
        }

        public int GetCount() {
            if (state == States.READY) {
                return servers.Count;
            }
            return 0;
        }

		public int GetMinPriority() {
			if (state == States.READY) {
				return minPriority;	
			}
			return 0;		
        }

		public string GetServerRoundRobin() {	
			if (state == States.READY) {
				return GetServerRoundRobin(currentPriority);
			}				
			return null;
		}

		public string GetServerRoundRobin(int priority) {
			LinkedListNode<Server> current = null;

			if (priority > currentPriority && priority <= minPriority) {
				currentPriority = priority;
			}
			if (priority > minPriority) {
				currentPriority = maxPriority;
			}

			if (lastServer == null) {
				current = servers.First;
			} else {
				current = lastServer.Next;
			}

			while (current.Next != null) {
				if (current.Value.priority == currentPriority) {					
					lastServer = current;
					return current.Value.site;
				}
					
				current = current.Next;
			}

			if (current.Value.priority == currentPriority) {
				lastServer = null;
				return current.Value.site;
			}
	
			return servers.First.Value.site;
		}			

		private void readFromNetwork() {	
			state = States.READING;
			string url = FILEURL + FILENAME;

			StartCoroutine(readServerList(url));
		}

		private void readFromSource() {					
			state = States.READING;

            string content = PlayerPrefs.GetString(SERVERSKEY);
            if (!String.IsNullOrEmpty(content)) {
                parseJson(content);
                state = States.READY;
                return;
            }

			foreach (Server s in localServers) {
				determinateMaxPriority(s.priority);
				addServer(s);
			}

			state = States.READY;
		}			

		private void readFromPlayerPrefs() {
			state = States.READING;
			string content = PlayerPrefs.GetString(SERVERSKEY);

			if (!String.IsNullOrEmpty(content)) {
				parseJson(content);
			}
		}

		private bool canReadFromNetwork() {
			string content = PlayerPrefs.GetString(SERVERSKEY);

			if (String.IsNullOrEmpty(content)) {
				return true;
			}

			string lastServerCheck = PlayerPrefs.GetString(LASTSERVERCHECK);		

			if (String.IsNullOrEmpty(lastServerCheck)) {
				return true;
			}

			DateTime lastDate =  DateTime.Parse(lastServerCheck);
			lastDate = lastDate.AddDays(1f);

			if (DateTime.Now.CompareTo(lastDate) > 0) {
				return true;
			}

			return false;
		}

		public bool IsReady() {
			return state == States.READY;
		}

		private void parseJson(string content) {			
			Dictionary<string,object> data = Json.Deserialize(content) as Dictionary<string,object>;

			if (data != null && data["servers"] != null) {
				List<object> serverList = data["servers"] as List<object>;

				if (serverList != null) {					
					servers = new LinkedList<Server>();

					foreach (object server in serverList) {
						Dictionary<string,object> mServer = server as Dictionary<string,object>;

						string url =  (string) mServer["site"];
						int mPriority = (int) ((long) mServer["priority"]);

						determinateMaxPriority(mPriority);

						addServer(url, mPriority);
					}

					if (servers.Count >= 1) {
						state = States.READY;	

						PlayerPrefs.SetString(SERVERSKEY, content);
						PlayerPrefs.SetString(LASTSERVERCHECK, DateTime.Now.ToString());
						PlayerPrefs.Save();
					}
				}
			}
		}

		private IEnumerator readServerList(string OriginUri) {		
			Uri uri = new Uri(OriginUri);
			bool isRemote = uri.Scheme == Uri.UriSchemeHttps;											

			UnityWebRequest req2 = UnityWebRequest.Get(OriginUri);				
			yield return req2.Send();

			if (req2.isError) {
				Debug.Log(req2.error);

				if (isRemote) {
					readFromSource();		
				}
				yield break;
			}
				
			parseJson(req2.downloadHandler.text);
		}

		private void determinateMaxPriority(int mPriority) {
			if (mPriority < currentPriority && mPriority > 0)  {				
				currentPriority = mPriority;
			}
			if (mPriority > currentPriority && mPriority > 0) {
				minPriority = mPriority;
			}
		}

		private void addServer(string site, int priority) {
			addServer(new Server(site, priority));
		}

		private void addServer(Server server) {
			if (Uri.IsWellFormedUriString(server.site, UriKind.Absolute)) {
				Uri uri = new Uri(server.site);

				if (server.priority > 0 && uri.Scheme == Uri.UriSchemeHttps) {
					LinkedListNode<Server> node = servers.AddLast(server);	
				}	
			}
		}			
	}	
}

