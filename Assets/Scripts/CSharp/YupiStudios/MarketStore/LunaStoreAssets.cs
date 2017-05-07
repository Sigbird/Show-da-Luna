using UnityEngine;
using System.Collections;

using Soomla.Store;
using System;

public class LunaStoreAssets : IStoreAssets {
    
	public const string FULLGAME_LTVG_PRODUCT_ID = "com.yupiplay.luna.fullgame";
    public const string FULLGAME_LTVG_ITEM_ID = "luna_fullgame_id";
    public static VirtualGood FULLGAME_LTVG
                            = new LifetimeVG(
								"O Show da Luna! Vamos Colorir. Versão Completa", 
                                "Permite o acesso ao conteúdo completo do jogo",
                                FULLGAME_LTVG_ITEM_ID, 
                                new PurchaseWithMarket(new MarketItem(FULLGAME_LTVG_PRODUCT_ID, 0.99))
                          );
	
	#region VIRTUAL_GOOD_STARS
	public const string STARS_CURRENCY_ID = "stars_currency";
	public static VirtualCurrency STAR_CURRENCY = new VirtualCurrency(
		"Stars",                               // Name
		"Stars Currency",                      // Description
		STARS_CURRENCY_ID                    // Item ID
		);

	public const string SIMPLE_STAR_PACK_ID = "stars_10_id";
	public const string SIMPLE_STAR_PACK_PRODUCT_ID = "com.yupiplay.luna.simplestarpack";
	public static VirtualCurrencyPack SIMPLE_STAR_PACK = new VirtualCurrencyPack(
		"10 Stars",                          // Name
		"10 Stars currency units",            // Description
		SIMPLE_STAR_PACK_ID,                       // Item ID
		10,                                  // Number of currencies in the pack
		STARS_CURRENCY_ID,                   // ID of the currency associated with this pack
		new PurchaseWithMarket(               // Purchase type (with real money $)
	                       SIMPLE_STAR_PACK_PRODUCT_ID,                   // Product ID
	                       0.99                                   // Price (in real money $)
	                       )
		);

	public const string SUPER_STAR_PACK_ID = "stars_60_id";
	public const string SUPER_STAR_PACK_PRODUCT_ID = "com.yupiplay.luna.superstarpack";
	public static VirtualCurrencyPack SUPER_STAR_PACK = new VirtualCurrencyPack(
		"60 Stars",                          // Name
		"60 Stars currency units",            // Description
		SUPER_STAR_PACK_ID,                       // Item ID
		60,                                  // Number of currencies in the pack
		STARS_CURRENCY_ID,                   // ID of the currency associated with this pack
		new PurchaseWithMarket(               // Purchase type (with real money $)
	                       SUPER_STAR_PACK_PRODUCT_ID,             // Product ID
	                       4.99                                   // Price (in real money $)
	                       )
		);

	public const string MEGA_STAR_PACK_ID = "stars_150_id";
	public const string MEGA_STAR_PACK_PRODUCT_ID = "com.yupiplay.luna.megastarpack";
	public static VirtualCurrencyPack MEGA_STAR_PACK = new VirtualCurrencyPack(
		"150 Stars",
		"150 Star currenty units",
		MEGA_STAR_PACK_ID,
		150,
		STARS_CURRENCY_ID,
		new PurchaseWithMarket(
				MEGA_STAR_PACK_PRODUCT_ID,
				9.99
			)
		);
	#endregion

	#region VIRTUAL_GOODS_MARKET

	//COLLECTION 01
	public const string COLLECTION_01_LTVG_PRODUCT_ID = "com.yupiplay.luna.videocollection01";		//ID do produto (como na loja)
	public const string COLLECTION01_LTVG_ITEM_ID = "collection_01_id";								//ID do item
	public static VirtualGood COLLECTION_01_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 01",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION01_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithMarket (												//Tipo de compra (real money)
							new MarketItem (
								COLLECTION_01_LTVG_PRODUCT_ID,									//ID do produto
		                		2.99))															//Preço
	);

	//VIDEO 01 
	public const string VIDEO_01_COL_01_LTVG_PRODUCT_ID = "com.yupiplay.luna.col01video02";
	public const string VIDEO_01_COL_01_LTVG_ITEM_ID = "video_01_col_01_id";
	public static VirtualGood VIDEO_01_COL_01_LTVG = 
		new LifetimeVG ("O Show da Luna! Cores para Cláudio Clipe Musical", 
		                "O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 01 Vídeo 02.O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 01 Vídeo 02.",
		                VIDEO_01_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithMarket (new MarketItem (VIDEO_01_COL_01_LTVG_PRODUCT_ID, 0.99))
		                );
	
	//VIDEO 02
	public const string VIDEO_02_COL_01_LTVG_PRODUCT_ID = "com.yupiplay.luna.col01video03";
	public const string VIDEO_02_COL_01_LTVG_ITEM_ID = "video_02_col_01_id";
	public static VirtualGood VIDEO_02_COL_01_LTVG = 
		new LifetimeVG ("O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical", 
		                "O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical. Coleção 01 Vídeo 03.",
		                VIDEO_02_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithMarket (new MarketItem (VIDEO_02_COL_01_LTVG_PRODUCT_ID, 0.99))
		                );
	
	//VIDEO 03
	public const string VIDEO_03_COL_01_LTVG_PRODUCT_ID = "com.yupiplay.luna.col01video04";
	public const string VIDEO_03_COL_01_LTVG_ITEM_ID = "video_03_col_01_id";
	public static VirtualGood VIDEO_03_COL_01_LTVG = 
		new LifetimeVG ("O Show da Luna! Afunda Ou Flutua? Clipe Musical",
		                "O Show da Luna! Afunda Ou Flutua? Clipe Musical. Coleção 01 Vídeo 04.",
		                VIDEO_03_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithMarket (new MarketItem (VIDEO_03_COL_01_LTVG_PRODUCT_ID, 0.99))
		                );
	
	//VIDEO 04
	public const string VIDEO_04_COL_01_LTVG_PRODUCT_ID = "com.yupiplay.luna.col01video05";
	public const string VIDEO_04_COL_01_LTVG_ITEM_ID = "video_04_col_01_id";
	public static VirtualGood VIDEO_04_COL_01_LTVG = 
		new LifetimeVG ("O Show da Luna! O Que Houve Com A Couve Clipe Musical",
		                "O Show da Luna! O Que Houve Com A Couve Clipe Musical. Coleção 01 Vídeo 05.",
		                VIDEO_04_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithMarket (new MarketItem (VIDEO_04_COL_01_LTVG_PRODUCT_ID, 0.99))
		                );

	#endregion VIRTUAL_GOODS_MARKET

	#region VirtualGoods_PurchaseWithVirtualItem
	//Full Game
	public const string STARS_FULL_GAME_ITEM_ID = "stars_luna_full_game";
	public static VirtualGood STARS_FULL_GAME = new LifetimeVG(
		"O Show da Luna! Vamos Colorir. Versão Completa",
		"Permite o acesso ao jogo completo",
		STARS_FULL_GAME_ITEM_ID,
		new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 20)
		);

	//Asas para Voar
	public const string MINIGAME_ASAS_ITEM_ID = "minigame_asas";
	public static VirtualGood MINIGAME_ASAS = new LifetimeVG(
			"O Show da Luna! Minigame Asas para Voar!",
			"Permite acesso ao minigame Asas para Voar!",
			MINIGAME_ASAS_ITEM_ID,
			new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

	//Corrida Caracol
	public const string MINIGAME_CARACOL_ITEM_ID = "minigame_caracol";
	public static VirtualGood MINIGAME_CARACOL = new LifetimeVG(
		"O Show da Luna! Minigame Asas para Voar!",
		"Permite acesso ao minigame Asas para Voar!",
		MINIGAME_CARACOL_ITEM_ID,
		new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

	//COLLECTION 01
	public const string STARS_COLLECTION01_LTVG_ITEM_ID = "stars_collection01_id";					//ID do item
	public static VirtualGood STARS_COLLECTION01_LTVG = 
		new LifetimeVG ("Coleção de vídeos 01",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o dispositivo.",	//Descriçao
		                STARS_COLLECTION01_LTVG_ITEM_ID,											//Id do item
		            	new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
	                );	

	//COLLECTION 02
	public const string COLLECTION02_LTVG_ITEM_ID = "collection_02_id";								//ID do item
	public static VirtualGood COLLECTION_02_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 02",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION02_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 03
	public const string COLLECTION03_LTVG_ITEM_ID = "collection_03_id";								//ID do item
	public static VirtualGood COLLECTION_03_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 03",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION03_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 04
	public const string COLLECTION04_LTVG_ITEM_ID = "collection_04_id";								//ID do item
	public static VirtualGood COLLECTION_04_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 04",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION04_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 05
	public const string COLLECTION05_LTVG_ITEM_ID = "collection_05_id";								//ID do item
	public static VirtualGood COLLECTION_05_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 05",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION05_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 06
	public const string COLLECTION06_LTVG_ITEM_ID = "collection_06_id";								//ID do item
	public static VirtualGood COLLECTION_06_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 06",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION06_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 07
	public const string COLLECTION07_LTVG_ITEM_ID = "collection_07_id";								//ID do item
	public static VirtualGood COLLECTION_07_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 07",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION07_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 08
	public const string COLLECTION08_LTVG_ITEM_ID = "collection_08_id";								//ID do item
	public static VirtualGood COLLECTION_08_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 08",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION08_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );

	//COLLECTION 09
	public const string COLLECTION09_LTVG_ITEM_ID = "collection_09_id";								//ID do item
	public static VirtualGood COLLECTION_09_LTVG = 
		new LifetimeVG ("O Show da Luna! Coleção de vídeos 09",										//Nome		
		                "Coleção com quatro vídeos que podem ser baixados para o seu dispositivo.",	//Descriçao
		                COLLECTION09_LTVG_ITEM_ID,													//Id do item
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 30)							//Preço
		                );



	//VIDEO 01 		
	public const string STARS_VIDEO_01_COL_01_LTVG_ITEM_ID = "stars_video_01_col_01_id";
	public static VirtualGood STARS_VIDEO_01_COL_01_LTVG = 
		new LifetimeVG ("Cores para Cláudio Clipe Musical", 
		                "Coleção 01 Vídeo 02",
		                STARS_VIDEO_01_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 02
	public const string STARS_VIDEO_02_COL_01_LTVG_ITEM_ID = "stars_video_02_col_01_id";
	public static VirtualGood STARS_VIDEO_02_COL_01_LTVG = 
		new LifetimeVG ("O Amarelo Que Ficou Verde Clipe Musical", 
		                "Coleção 01 Vídeo 03",
		                STARS_VIDEO_02_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 03
	public const string STARS_VIDEO_03_COL_01_LTVG_ITEM_ID = "stars_video_03_col_01_id";
	public static VirtualGood STARS_VIDEO_03_COL_01_LTVG = 
		new LifetimeVG ("Afunda Ou Flutua? Clipe Musical",
		                "Coleção 01 Vídeo 04",
		                STARS_VIDEO_03_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 04
	public const string STARS_VIDEO_04_COL_01_LTVG_ITEM_ID = "stars_video_04_col_01_id";
	public static VirtualGood STARS_VIDEO_04_COL_01_LTVG = 
		new LifetimeVG ("O Que Houve Com A Couve Clipe Musical",
		                "Coleção 01 Vídeo 05",
		                STARS_VIDEO_04_COL_01_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );


	//VIDEO 01 C2
	public const string VIDEO_01_COL_02_LTVG_ITEM_ID = "video_01_col_02_id";
	public static VirtualGood VIDEO_01_COL_02_LTVG = 
		new LifetimeVG ("O Show da Luna! Cores para Cláudio Clipe Musical", 
		                "O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 02 Vídeo 02. O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 02 Vídeo 02.",
		                VIDEO_01_COL_02_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 02 C2
	public const string VIDEO_02_COL_02_LTVG_ITEM_ID = "video_02_col_02_id";
	public static VirtualGood VIDEO_02_COL_02_LTVG = 
		new LifetimeVG ("O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical", 
		                "O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical. Coleção 02 Vídeo 03.",
		                VIDEO_02_COL_02_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 03 C2
	public const string VIDEO_03_COL_02_LTVG_ITEM_ID = "video_03_col_02_id";
	public static VirtualGood VIDEO_03_COL_02_LTVG = 
		new LifetimeVG ("O Show da Luna! Afunda Ou Flutua? Clipe Musical",
		                "O Show da Luna! Afunda Ou Flutua? Clipe Musical. Coleção 02 Vídeo 04.",
		                VIDEO_03_COL_02_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 04 C2
	public const string VIDEO_04_COL_02_LTVG_ITEM_ID = "video_04_col_02_id";
	public static VirtualGood VIDEO_04_COL_02_LTVG = 
		new LifetimeVG ("O Show da Luna! O Que Houve Com A Couve Clipe Musical",
		                "O Show da Luna! O Que Houve Com A Couve Clipe Musical. Coleção 02 Vídeo 05.",
		                VIDEO_04_COL_02_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	//VIDEO 01 C3
	public const string VIDEO_01_COL_03_LTVG_ITEM_ID = "video_01_col_03_id";
	public static VirtualGood VIDEO_01_COL_03_LTVG = 
		new LifetimeVG ("O Show da Luna! Cores para Cláudio Clipe Musical", 
		                "O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 03 Vídeo 02.O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 03 Vídeo 02.",
		                VIDEO_01_COL_03_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 02 C3
	public const string VIDEO_02_COL_03_LTVG_ITEM_ID = "video_02_col_03_id";
	public static VirtualGood VIDEO_02_COL_03_LTVG = 
		new LifetimeVG ("O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical", 
		                "O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical. Coleção 03 Vídeo 03.",
		                VIDEO_02_COL_03_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 03 C3
	public const string VIDEO_03_COL_03_LTVG_ITEM_ID = "video_03_col_03_id";
	public static VirtualGood VIDEO_03_COL_03_LTVG = 
		new LifetimeVG ("O Show da Luna! Afunda Ou Flutua? Clipe Musical",
		                "O Show da Luna! Afunda Ou Flutua? Clipe Musical. Coleção 03 Vídeo 04.",
		                VIDEO_03_COL_03_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 04 C3
	public const string VIDEO_04_COL_03_LTVG_ITEM_ID = "video_04_col_03_id";
	public static VirtualGood VIDEO_04_COL_03_LTVG = 
		new LifetimeVG ("O Show da Luna! O Que Houve Com A Couve Clipe Musical",
		                "O Show da Luna! O Que Houve Com A Couve Clipe Musical. Coleção 03 Vídeo 05.",
		                VIDEO_04_COL_03_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	//VIDEO 01 C4

	public const string VIDEO_01_COL_04_LTVG_ITEM_ID = "video_01_col_04_id";
	public static VirtualGood VIDEO_01_COL_04_LTVG = 
		new LifetimeVG ("O Show da Luna! Cores para Cláudio Clipe Musical", 
		                "O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 04 Vídeo 02.O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 04 Vídeo 02.",
		                VIDEO_01_COL_04_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 02 C4
	public const string VIDEO_02_COL_04_LTVG_ITEM_ID = "video_02_col_04_id";
	public static VirtualGood VIDEO_02_COL_04_LTVG = 
		new LifetimeVG ("O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical", 
		                "O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical. Coleção 04 Vídeo 03.",
		                VIDEO_02_COL_04_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 03 C4
	public const string VIDEO_03_COL_04_LTVG_ITEM_ID = "video_03_col_04_id";
	public static VirtualGood VIDEO_03_COL_04_LTVG = 
		new LifetimeVG ("O Show da Luna! Afunda Ou Flutua? Clipe Musical",
		                "O Show da Luna! Afunda Ou Flutua? Clipe Musical. Coleção 04 Vídeo 04.",
		                VIDEO_03_COL_04_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 04 C4
	public const string VIDEO_04_COL_04_LTVG_ITEM_ID = "video_04_col_04_id";
	public static VirtualGood VIDEO_04_COL_04_LTVG = 
		new LifetimeVG ("O Show da Luna! O Que Houve Com A Couve Clipe Musical",
		                "O Show da Luna! O Que Houve Com A Couve Clipe Musical. Coleção 04 Vídeo 05.",
		                VIDEO_04_COL_04_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	//VIDEO 01 C5
	public const string VIDEO_01_COL_05_LTVG_ITEM_ID = "video_01_col_05_id";
	public static VirtualGood VIDEO_01_COL_05_LTVG = 
		new LifetimeVG ("O Show da Luna! Cores para Cláudio Clipe Musical", 
		                "O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 05 Vídeo 02.O Show da Luna! Cores para Cláudio Clipe Musical. Coleção 05 Vídeo 02.",
		                VIDEO_01_COL_05_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 02 C5
	public const string VIDEO_02_COL_05_LTVG_ITEM_ID = "video_02_col_05_id";
	public static VirtualGood VIDEO_02_COL_05_LTVG = 
		new LifetimeVG ("O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical", 
		                "O Show da Luna! O Amarelo Que Ficou Verde Clipe Musical. Coleção 05 Vídeo 03.",
		                VIDEO_02_COL_05_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 03 C5
	public const string VIDEO_03_COL_05_LTVG_ITEM_ID = "video_03_col_05_id";
	public static VirtualGood VIDEO_03_COL_05_LTVG = 
		new LifetimeVG ("O Show da Luna! Afunda Ou Flutua? Clipe Musical",
		                "O Show da Luna! Afunda Ou Flutua? Clipe Musical. Coleção 05 Vídeo 04.",
		                VIDEO_03_COL_05_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );
	
	//VIDEO 04 C5
	public const string VIDEO_04_COL_05_LTVG_ITEM_ID = "video_04_col_05_id";
	public static VirtualGood VIDEO_04_COL_05_LTVG = 
		new LifetimeVG ("O Show da Luna! O Que Houve Com A Couve Clipe Musical",
		                "O Show da Luna! O Que Houve Com A Couve Clipe Musical. Coleção 05 Vídeo 05.",
		                VIDEO_04_COL_05_LTVG_ITEM_ID,
		                new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		                );

	//VIDEO 05 C5
	//TODO MUDAR PRO NOME REAL
	public const string VIDEO_05_COL_05_LTVG_ITEM_ID = "video_05_col_05_id";
	public static VirtualGood VIDEO_05_COL_05_LTVG = new LifetimeVG(
			"O Show da Luna! Video 05 Coleçao 05",
			"O Show da Luna! Video 05 Coleçao 05",
			VIDEO_05_COL_05_LTVG_ITEM_ID,
			new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

	//VIDEO 01 C6
	public const string VIDEO_01_COL_06_LTVG_ITEM_ID = "video_01_col_06_id";
	public static VirtualGood VIDEO_01_COL_06_LTVG = new LifetimeVG(
		"O Show da Luna! Desenhos do Ceu",
		"O Show da Luna! Video 01 Coleçao 06",
		VIDEO_01_COL_06_LTVG_ITEM_ID,
		new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

	//VIDEO 02 C6
	public const string VIDEO_02_COL_06_LTVG_ITEM_ID = "video_02_col_06_id";
	public static VirtualGood VIDEO_02_COL_06_LTVG = new LifetimeVG(
		"O Show da Luna! Bigodudos",
		"O Show da Luna! Video 02 Coleçao 06",
		VIDEO_02_COL_06_LTVG_ITEM_ID,
		new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

	//VIDEO 03 C6
	public const string VIDEO_03_COL_06_LTVG_ITEM_ID = "video_03_col_06_id";
	public static VirtualGood VIDEO_03_COL_06_LTVG = new LifetimeVG(
		"O Show da Luna! Subindo!",
		"O Show da Luna! Video 03 Coleçao 06",
		VIDEO_03_COL_06_LTVG_ITEM_ID,
		new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

	//VIDEO 04 C6
	public const string VIDEO_04_COL_06_LTVG_ITEM_ID = "video_04_col_06_id";
	public static VirtualGood VIDEO_04_COL_06_LTVG = new LifetimeVG(
		"O Show da Luna! Doce ou Salgado?",
		"O Show da Luna! Video 04 Coleçao 06",
		VIDEO_04_COL_06_LTVG_ITEM_ID,
		new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
		);

    //Video 01 C7    
    public const string VIDEO_01_COL_07_LTVG_ITEM_ID = "video_01_col_07_id";
    public static VirtualGood VIDEO_01_COL_07_LTVG = new LifetimeVG(
        "O Show da Luna! Dó Ré Mi Flauta",
        "O Show da Luna! Video 01 Coleçao 07",
        VIDEO_01_COL_07_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 02 C7    
    public const string VIDEO_02_COL_07_LTVG_ITEM_ID = "video_02_col_07_id";
    public static VirtualGood VIDEO_02_COL_07_LTVG = new LifetimeVG(
        "O Show da Luna! Cola de Largatixa?",
        "O Show da Luna! Video 02 Coleçao 07",
        VIDEO_02_COL_07_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 03 C7    
    public const string VIDEO_03_COL_07_LTVG_ITEM_ID = "video_03_col_07_id";
    public static VirtualGood VIDEO_03_COL_07_LTVG = new LifetimeVG(
        "O Show da Luna! Bem Vinda, Neve!",
        "O Show da Luna! Video 03 Coleçao 07",
        VIDEO_03_COL_07_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 04 C7    
    public const string VIDEO_04_COL_07_LTVG_ITEM_ID = "video_04_col_07_id";
    public static VirtualGood VIDEO_04_COL_07_LTVG = new LifetimeVG(
        "O Show da Luna! Pula-Pula Pipoca",
        "O Show da Luna! Video 04 Coleçao 07",
        VIDEO_04_COL_07_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 05 C7    
    public const string VIDEO_05_COL_07_LTVG_ITEM_ID = "video_05_col_07_id";
    public static VirtualGood VIDEO_05_COL_07_LTVG = new LifetimeVG(
        "O Show da Luna! Um Conto de Caudas",
        "O Show da Luna! Video 05 Coleçao 07",
        VIDEO_05_COL_07_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 01 C8    
    public const string VIDEO_01_COL_08_LTVG_ITEM_ID = "video_01_col_08_id";
    public static VirtualGood VIDEO_01_COL_08_LTVG = new LifetimeVG(
        "O Show da Luna! Tecendo Teias",
        "O Show da Luna! Video 01 Coleçao 08",
        VIDEO_01_COL_08_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 02 C8    
    public const string VIDEO_02_COL_08_LTVG_ITEM_ID = "video_02_col_08_id";
    public static VirtualGood VIDEO_02_COL_08_LTVG = new LifetimeVG(
        "O Show da Luna! Um Trovão, Dois Trovões, Três!",
        "O Show da Luna! Video 02 Coleçao 08",
        VIDEO_02_COL_08_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 03 C8    
    public const string VIDEO_03_COL_08_LTVG_ITEM_ID = "video_03_col_08_id";
    public static VirtualGood VIDEO_03_COL_08_LTVG = new LifetimeVG(
        "O Show da Luna! Um Recadinho do Algodão",
        "O Show da Luna! Video 03 Coleçao 08",
        VIDEO_03_COL_08_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 04 C8    
    public const string VIDEO_04_COL_08_LTVG_ITEM_ID = "video_04_col_08_id";
    public static VirtualGood VIDEO_04_COL_08_LTVG = new LifetimeVG(
        "O Show da Luna! O Grande AStro",
        "O Show da Luna! Video 04 Coleçao 08",
        VIDEO_04_COL_08_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 05 C8    
    public const string VIDEO_05_COL_08_LTVG_ITEM_ID = "video_05_col_08_id";
    public static VirtualGood VIDEO_05_COL_08_LTVG = new LifetimeVG(
        "O Show da Luna! Parece Mas Não É",
        "O Show da Luna! Video 05 Coleçao 08",
        VIDEO_05_COL_08_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 01 C9    
    public const string VIDEO_01_COL_09_LTVG_ITEM_ID = "video_01_col_09_id";
    public static VirtualGood VIDEO_01_COL_09_LTVG = new LifetimeVG(
        "O Show da Luna! A Maravilhosa Floresta de Chocolate",
        "O Show da Luna! Video 01 Coleçao 09",
        VIDEO_01_COL_09_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 02 C9    
    public const string VIDEO_02_COL_09_LTVG_ITEM_ID = "video_02_col_09_id";
    public static VirtualGood VIDEO_02_COL_09_LTVG = new LifetimeVG(
        "O Show da Luna! Bicho da Seda",
        "O Show da Luna! Video 02 Coleçao 09",
        VIDEO_02_COL_09_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 03 C9    
    public const string VIDEO_03_COL_09_LTVG_ITEM_ID = "video_03_col_09_id";
    public static VirtualGood VIDEO_03_COL_09_LTVG = new LifetimeVG(
        "O Show da Luna! Dirigir, Rodar e Deslizar",
        "O Show da Luna! Video 03 Coleçao 09",
        VIDEO_03_COL_09_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 04 C9    
    public const string VIDEO_04_COL_09_LTVG_ITEM_ID = "video_04_col_09_id";
    public static VirtualGood VIDEO_04_COL_09_LTVG = new LifetimeVG(
        "O Show da Luna! Assombrados",
        "O Show da Luna! Video 04 Coleçao 09",
        VIDEO_04_COL_09_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    //Video 05 C9    
    public const string VIDEO_05_COL_09_LTVG_ITEM_ID = "video_05_col_09_id";
    public static VirtualGood VIDEO_05_COL_09_LTVG = new LifetimeVG(
        "O Show da Luna! Eco, Eco, Eco",
        "O Show da Luna! Video 05 Coleçao 09",
        VIDEO_05_COL_09_LTVG_ITEM_ID,
        new PurchaseWithVirtualItem(STARS_CURRENCY_ID, 10)
        );

    #endregion VirtualGoods_PurchaseWithVirtualItem

    #region IStoreAssets Impl
    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] {};
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] { 
			STAR_CURRENCY};
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { 
			SIMPLE_STAR_PACK, SUPER_STAR_PACK, MEGA_STAR_PACK
        };
    }

    public VirtualGood[] GetGoods()
    {
		return new VirtualGood[] {
			FULLGAME_LTVG, 
			STARS_FULL_GAME,
			COLLECTION_01_LTVG, 
			VIDEO_01_COL_01_LTVG, 
			VIDEO_02_COL_01_LTVG,
			VIDEO_03_COL_01_LTVG, 
			VIDEO_04_COL_01_LTVG,
			STARS_COLLECTION01_LTVG,
			COLLECTION_02_LTVG,
			COLLECTION_03_LTVG,
			COLLECTION_04_LTVG,
			COLLECTION_05_LTVG,
			COLLECTION_06_LTVG,
			COLLECTION_07_LTVG,
            COLLECTION_08_LTVG,
            COLLECTION_09_LTVG,
            STARS_VIDEO_01_COL_01_LTVG,
			STARS_VIDEO_02_COL_01_LTVG,
			STARS_VIDEO_03_COL_01_LTVG,
			STARS_VIDEO_04_COL_01_LTVG,
			VIDEO_01_COL_02_LTVG,
			VIDEO_02_COL_02_LTVG,
			VIDEO_03_COL_02_LTVG,
			VIDEO_04_COL_02_LTVG,
			VIDEO_01_COL_03_LTVG,
			VIDEO_02_COL_03_LTVG,
			VIDEO_03_COL_03_LTVG,
			VIDEO_04_COL_03_LTVG,
			VIDEO_01_COL_04_LTVG,
			VIDEO_02_COL_04_LTVG,
			VIDEO_03_COL_04_LTVG,
			VIDEO_04_COL_04_LTVG,
			VIDEO_01_COL_05_LTVG,
			VIDEO_02_COL_05_LTVG,
			VIDEO_03_COL_05_LTVG,
			VIDEO_04_COL_05_LTVG,
			VIDEO_05_COL_05_LTVG,
			VIDEO_01_COL_06_LTVG,
			VIDEO_02_COL_06_LTVG,
			VIDEO_03_COL_06_LTVG,
			VIDEO_04_COL_06_LTVG,
            VIDEO_01_COL_07_LTVG,
            VIDEO_02_COL_07_LTVG,
            VIDEO_03_COL_07_LTVG,
            VIDEO_04_COL_07_LTVG,
            VIDEO_05_COL_07_LTVG,
            VIDEO_01_COL_08_LTVG,
            VIDEO_02_COL_08_LTVG,
            VIDEO_03_COL_08_LTVG,
            VIDEO_04_COL_08_LTVG,
            VIDEO_05_COL_08_LTVG,
            VIDEO_01_COL_09_LTVG,
            VIDEO_02_COL_09_LTVG,
            VIDEO_03_COL_09_LTVG,
            VIDEO_04_COL_09_LTVG,
            VIDEO_05_COL_09_LTVG,
            MINIGAME_ASAS,
			MINIGAME_CARACOL
		};
    }

    public int GetVersion()
    {
        return 26;
    }
    #endregion //IStoreAssets Impl


}
