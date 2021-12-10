import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseOverviewComponent } from './course-overview.component';

describe('CourseOverviewComponent', () => {
  let component: CourseOverviewComponent;
  let httpclient: HttpClientTestingModule;
  let httpTestingController: HttpTestingController;
  let fixture: ComponentFixture<CourseOverviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [CourseOverviewComponent]
    });

    httpclient = TestBed.get(HttpClientTestingModule);
    httpTestingController = TestBed.get(HttpTestingController);
    fixture = TestBed.createComponent(CourseOverviewComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  }
  );
});
