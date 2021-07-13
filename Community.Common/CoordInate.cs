using System;

namespace Community.Common
{
    public class CoordInate
    {
        private const double EARTH_RADIUS = 6378.137;//地球半径
        private static double rad(double d)
        { 
            var kk= Math.PI;
            var h = d * kk;
            double k = h / 180.0;
            return k;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
            Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }
    }
}
