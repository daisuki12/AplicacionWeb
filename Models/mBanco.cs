﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWeb.Models
{
    public class mBanco
    {
        public int idBanco { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha_registro { get; set; }
    }
}