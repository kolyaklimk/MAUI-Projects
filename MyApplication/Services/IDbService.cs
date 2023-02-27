﻿using MyApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Services
{
    public interface IDbService
    {
        IEnumerable<SportTeam> GetAllSportTeams();
        IEnumerable<Member> GetSportTeamMembers(int id);
        void Init();
    }
}
