using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TrailLocker.Models;

namespace TrailLocker.Authentication
{
    public class MockAuthenticatedUserProvider : IAuthenticatedUserProvider
    {
        private User user;

        public MockAuthenticatedUserProvider(User user)
        {
            this.user = user;
        }
        
        public User AuthenticatedUser
        {
            get
            {
                return user;
            }
        }
    }
}