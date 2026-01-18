using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace LibraryManagement.DAL.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(p=>p.PersonID);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
                

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);


            builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

            builder.Property(p => p.IsActive)
                .IsRequired();

            // indexes
            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.HasIndex(p => p.Phone)
                .IsUnique();
            builder.HasData(
                new Person
                {
                    PersonID = 1,
                    FirstName = "Ahmed",
                    LastName = "Alami",
                    Email = "ahmed.alami@gmail.com",
                    Phone = "0612345678",
                    Address = "Agdal, Rabat",
                    City = "Rabat",
                    Gender = 'M',
                    IsActive = true
                },
new Person
{
    PersonID = 2,
    FirstName = "Fatima",
    LastName = "Zahra",
    Email = "fatima.ezzahra@outlook.com",
    Phone = "0623456789",
    Address = "Gueliz, Marrakech",
    City = "Marrakech",
    Gender = 'F',
    IsActive = true
},
new Person
{
    PersonID = 3,
    FirstName = "Youssef",
    LastName = "Idrissi",
    Email = "youssef.idrissi@yahoo.com",
    Phone = "0634567890",
    Address = "Maarif, Casablanca",
    City = "Casablanca",
    Gender = 'M',
    IsActive = true
},
new Person
{
    PersonID = 4,
    FirstName = "Sanaa",
    LastName = "Bennani",
    Email = "sanaa.bennani@gmail.com",
    Phone = "0645678901",
    Address = "Ville Nouvelle, Fes",
    City = "Fes",
    Gender = 'F',
    IsActive = true
},
new Person
{
    PersonID = 5,
    FirstName = "Omar",
    LastName = "Mansouri",
    Email = "omar.mansouri@hotmail.com",
    Phone = "0656789012",
    Address = "Malabata, Tanger",
    City = "Tanger",
    Gender = 'M',
    IsActive = true
},
new Person
{
    PersonID = 6,
    FirstName = "Laila",
    LastName = "Tazi",
    Email = "laila.tazi@gmail.com",
    Phone = "0667890123",
    Address = "Hay Salam, Agadir",
    City = "Agadir",
    Gender = 'F',
    IsActive = true
},
new Person
{
    PersonID = 7,
    FirstName = "Karim",
    LastName = "Sabbahi",
    Email = "karim.sabbahi@icloud.com",
    Phone = "0678901234",
    Address = "Ouled Ayad, Beni Mellal",
    City = "Beni Mellal",
    Gender = 'M',
    IsActive = true
},
new Person
{
    PersonID = 8,
    FirstName = "Meryem",
    LastName = "Fassi",
    Email = "meryem.fassi@gmail.com",
    Phone = "0689012345",
    Address = "Nansria, Oujda",
    City = "Oujda",
    Gender = 'F',
    IsActive = true
},
new Person
{
    PersonID = 9,
    FirstName = "Hamza",
    LastName = "Radi",
    Email = "hamza.radi@live.com",
    Phone = "0690123456",
    Address = "Dakhla, Meknes",
    City = "Meknes",
    Gender = 'M',
    IsActive = true
},
new Person
{
    PersonID = 10,
    FirstName = "Salma",
    LastName = "Amrani",
    Email = "salma.amrani@gmail.com",
    Phone = "0601234567",
    Address = "Mohammedia Center",
    City = "Mohammedia",
    Gender = 'F',
    IsActive = true
},
                 new Person
                 {
                     PersonID = 11,
                     FirstName = "Abdenabi",
                     LastName = "Khaldi",
                     Email = "Freeh11@gmail.com",
                     Phone = "0644353219",
                     Address = "Ait Alla , Tabia , Azilal",
                     City = "Azilal",
                     Gender = 'M',
                     IsActive = true
                 }
                  
                );
        }

        
    }
}
