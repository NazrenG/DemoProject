﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Abstract
{
    public interface IQuizService
    {

        Task<List<Quiz>> Quizzes(); 
    }
}
