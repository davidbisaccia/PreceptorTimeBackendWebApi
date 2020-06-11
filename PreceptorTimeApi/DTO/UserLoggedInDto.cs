using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreceptorTime.Api.DTO
{
    public class UserLoggedInDto
    {
        //constructor(public email: string, public id: string, public displayName: string, 
        //        public accountType: string, private _token: string, private _tokenExpirationDate: Date)
        public string Email { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string AccountType { get; set; }
        public string Token { get; set; }
        public string TokenExpirationDate { get; set; }
    }
}
