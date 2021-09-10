﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketBooking.Models
{
    public class BookingContext : DbContext
    {

     public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> TodoItems { get; set; }




    }
}
