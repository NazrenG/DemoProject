﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Abstract;
using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    public class TaskAssigne:BaseEntity ,IEntity
    { 
        public int TaskForUserId { get; set; }
        public int UserId { get; set; } 
    }
}
