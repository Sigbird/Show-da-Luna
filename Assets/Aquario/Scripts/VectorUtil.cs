using UnityEngine;

namespace YupiPlay.Luna.Aquario 
{
    public static class VectorUtil {
        public static Vector2 ParalellToGravity(Vector2 pos) {
            return (Physics2D.gravity - pos) + pos;
        }

        //The matrix determinant is an abstraction that equals to the linear
        //equation
        //a and b make a line, c is the query point
        public static float Determinant(Vector2 a, Vector2 b, Vector2 c) {
            Vector2 line = b - a;
            Vector2 point = c - a;
            return line.x * point.y - point.x * line.y;

            //reference
            //return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        }

        //A determinante da matriz dos vetores é uma abstração que 
        //corresponde à equação linear dada dois pontos
        // vetores a e b são diferenças entre pontos
        public static float MatrixDeterminant(Vector2 a, Vector2 b) {
            return a.x * b.y - b.x * a.y;
        }


        //equação da reta obtida através proporção das tangentes
        public static float LinearValue(Vector2 a, Vector2 b, Vector2 c) {
            return (c.y - a.y) - (c.x - a.x) * ((b.y - a.y) / (b.x - a.x));
        }
    }
}