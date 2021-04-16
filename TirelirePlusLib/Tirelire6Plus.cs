﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TireLireLib;

namespace TirelirePlusLib
{
    public class Tirelire6Plus : Tirelire6
    {
        public bool Init(decimal montant = 0)
        {
            if (montant >= 0)
            {
                _ = this.Vider();
                _ = this.Déposer(montant);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
