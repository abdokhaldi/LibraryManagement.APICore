using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BorrowingDTOs
{
    public class BorrowingForCreationDTO
    {
            
            public required string Barcode { get; set; }
            public required string NationalNumber { get; set; }
            public required decimal InitialFees { get; set; }
            public required DateTime DueDate { get; set; }
        }

    }

