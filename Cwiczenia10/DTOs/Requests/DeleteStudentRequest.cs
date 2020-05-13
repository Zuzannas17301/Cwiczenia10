﻿using System.ComponentModel.DataAnnotations;

namespace Cwiczenia10.DTOs
{
    public class DeleteStudentRequest
    {
        [Required] public string IndexNumber { get; set; }
    }
}