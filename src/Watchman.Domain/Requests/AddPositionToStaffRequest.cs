﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchman.Domain.Requests {

  public class AddPositionToStaffRequest {
    public Guid StaffId { get; set; }
    public int StaffPositionId { get; set; }
  }
}