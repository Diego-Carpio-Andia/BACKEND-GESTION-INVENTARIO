using AutoMapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//clase que se comunicara de curso a cursoDTO
namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            //que clases se van a mapear
            //como cursoDTO espera una lista de instructores
            //haremos un mapeo manual para validar las listas de instructores deCurso a CursoDto
            //CreateMap<Curso, CursoDto>()
            //    //public ICollection<InstructorDto> Instructores { get; set; }
            //    .ForMember(x => x.Instructores/*LLENADO*/,/*Quien lo llenara*/ y => y.MapFrom(z => z.InstructoresLink.Select(a => a.Instructor).ToList()))
            //    //public PrecioDto Precio { get; set; }
            //    .ForMember(x => x.Precio, y => y.MapFrom(y => y.PrecioPromocion))
            //    //public  ICollection<ComentarioDto> Comentarios { get; set; } 
            //    .ForMember(x => x.Comentarios, y => y.MapFrom(z => z.ComentarioLista));

            //CreateMap<Curso, CursoInstructorDto>();
            //CreateMap<Instructor, InstructorDto>();
            ////ahora para comentarios y preciio ya que se mostrara
            //CreateMap<Comentario, ComentarioDto>();
            //CreateMap<Precio, PrecioDto>();
        }
    }
}
