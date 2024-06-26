﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Documento
    {
        public Guid DocumentoId { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public byte[] Contenido { get; set; }
        public DateTime FechaCreacion {  get; set; }
        //Vinculacion de imagen a objeto referencia
        public Guid ObjetoReferencia {  get; set; }
    }
}
