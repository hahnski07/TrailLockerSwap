using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TrailLocker.Models;

namespace TrailLocker.Authentication
{
    public interface IAuthenticatedUserProvider
    {
        User AuthenticatedUser { get; }
    }
}