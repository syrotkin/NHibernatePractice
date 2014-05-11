﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernatePractice1.Domain
{
    public class OsyCat
    {
        private string _id;
        private string _name;
        private char _sex;
        private float _weight;

        public OsyCat()
        {
        }
        
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual char Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public virtual float Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
    }
}
