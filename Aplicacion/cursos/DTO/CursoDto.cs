﻿using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.cursos.DTO
{
    //Representa la data que se devolvera al cliente
    public class CursoDto
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }
        public ICollection<InstructorDto> Instructores { get; set; }
        public PrecioDto Precio { get; set; }
        public  ICollection<ComentarioDto> Comentarios { get; set; }    
        public DateTime? FechaCreacion {  get; set; }   

    }
}
