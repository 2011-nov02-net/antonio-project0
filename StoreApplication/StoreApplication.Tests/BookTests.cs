﻿using StoreApplication.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApplication.Tests
{
    public class BookTests
    {
        [Fact]
        public void CanFindABookThatExists()
        {
            var newBook = new Book();
            newBook.ISBN = "111";
            Book.Library.Add(newBook);

            Assert.True(Book.CheckIfIsValidIsbn("111"), "The book should exist!");
        }

        [Fact]
        public void CantFindABookThatDoesntExist()
        {
            Assert.False(Book.CheckIfIsValidIsbn("0141"), "The book should not exist!");
        }
    }
}
