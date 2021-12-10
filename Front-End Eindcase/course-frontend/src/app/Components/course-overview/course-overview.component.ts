import { Component, OnInit } from '@angular/core';
import { CourseOverviewService } from 'src/app/Services/course-overview.service';

@Component({
  selector: 'course-overview',
  templateUrl: './course-overview.component.html',
})
export class CourseOverviewComponent implements OnInit {

  constructor(public service: CourseOverviewService) { }

  ngOnInit(): void {
    this.service.refreshCursusLijst();
  }
}
