using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringTime
{
    class StudentInfo
    {
        // Fields
        private string name, hometown, favoriteFood, major, favoriteColor;

        // Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Hometown
        {
            get { return hometown; }
            set { hometown = value; }
        }
        public string FavoriteFood
        {
            get { return favoriteFood; }
            set { favoriteFood = value; }
        }
        public string Major
        {
            get { return major; }
            set { major = value; }
        }
        public string FavoriteColor
        {
            get { return favoriteColor; }
            set { favoriteColor = value; }
        }

        // Constructor
        public StudentInfo(string n, string h, string ff, string m, string fc)
        {
            name = n;
            hometown = h;
            favoriteFood = ff;
            major = m;
            favoriteColor = fc;
        }
    }
}
