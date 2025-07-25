﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace MoreSpears
{
    public static class Registry
    {
        public static bool registered = false;
        public static AbstractPhysicalObject.AbstractObjectType tranqSpear;
        public static AbstractPhysicalObject.AbstractObjectType heavySpear;
        public static AbstractPhysicalObject.AbstractObjectType iceSpear;
        public static FAtlas SpearAtlas;
        //public static Dictionary<string, AbstractPhysicalObject.AbstractObjectType> Spears = new Dictionary<string, AbstractPhysicalObject.AbstractObjectType>()
        //{
        //    { "TranqSpear", null },
        //    { "HeavySpear", null }
        //}; // TESTCODE

        public static void RegisterValues()
        {

            //for (int i = 0; i < Spears.Count; i++) // TESTCODE
            //{
            //    string elm = Spears.ElementAt(i).Key;
            //    Spears[elm] = new AbstractPhysicalObject.AbstractObjectType(elm, true);
            //}
            tranqSpear = new AbstractPhysicalObject.AbstractObjectType("TranqSpear", true);
            heavySpear = new AbstractPhysicalObject.AbstractObjectType("HeavySpear", true);
            iceSpear = new AbstractPhysicalObject.AbstractObjectType("IceSpear", true);
            registered = true;
        }

        public static void UnregisterValues()
        {
            //List<AbstractPhysicalObject.AbstractObjectType> unreglist = new List<AbstractPhysicalObject.AbstractObjectType>();

            //for (int i = 0; i < Spears.Count; i++) // TESTCODE
            //{
            //    var elm = Spears.ElementAt(i).Key;
            //    unreglist.Add(Spears[elm]);
            //    Spears[elm] = null;
            //}

            //for (int i = 0; i < unreglist.Count; i++)
            //{
            //    unreglist[i].Unregister();
            //}
            AbstractPhysicalObject.AbstractObjectType tranqspear = tranqSpear;
            AbstractPhysicalObject.AbstractObjectType heavyspear = heavySpear;
            AbstractPhysicalObject.AbstractObjectType icespear = iceSpear;

            tranqspear?.Unregister();
            heavyspear?.Unregister();
            icespear?.Unregister();
            tranqSpear = null;
            heavySpear = null;
            iceSpear = null;
            registered = false;
        }
    }
}
