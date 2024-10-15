﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class QuizService : IQuizService
    {
        private readonly IQuizDal dal;

        public QuizService(IQuizDal dal)
        {
            this.dal = dal;
        }

        public async Task<List<Quiz>> Quizzes()
        {
            return await dal.GetAll();
        }
    }
}