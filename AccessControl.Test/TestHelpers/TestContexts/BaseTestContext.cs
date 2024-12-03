using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessControl.Infrastructure;
using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Test.TestHelpers.TestContexts
{
    public class BaseTestContext
    {

        public static readonly Faker _faker = new Faker();
        protected AccessControlContext _context { get; }

        
        public BaseTestContext(AccessControlContext context)
        {
            _context = context;
        }

        public static int GetDefaultId()
        {
            return _faker.Random.Int(1, 2000);
        }
        public static string GetEmailAddress()
        {
            return _faker.Internet.ExampleEmail().ClampLength(15);
        }

        public static string GetUserName()
        {
            return _faker.Internet.UserName();
        }
        public static string GetUserFullName()
        {
            return _faker.Name.FullName();
        }

        public static string GetRandomString(int length)
        {
            return _faker.Random.AlphaNumeric(length);
        }

    }
}
