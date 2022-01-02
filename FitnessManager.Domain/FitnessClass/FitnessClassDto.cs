using System;
using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.FitnessClass
{
    public class FitnessClassDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int MaximumParticipants { get; set; }
        public  InstructorForFitnessClassDto DefaultInstructor { get; set; }
        public  ICollection<FitnessClassEnrolmentsForFitnessClassDto> Enrolments { get; set; }
    }

    public class FitnessClubMapping : Profile
    {
        public FitnessClubMapping()
        {
            CreateMap<FitnessClassEntity, FitnessClassDto>();
            CreateMap<InstructorEntity, InstructorForFitnessClassDto>();
            CreateMap<FitnessClassEnrolmentsEntity, FitnessClassEnrolmentsForFitnessClassDto>();
        }
    }
}