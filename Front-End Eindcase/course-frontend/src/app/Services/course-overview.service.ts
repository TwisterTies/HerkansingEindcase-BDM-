import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CourseOverviewModel } from '../Models/course-overview.model';

@Injectable({
  providedIn: 'root'
})
export class CourseOverviewService {

  constructor(private http: HttpClient) { }
  readonly baseURL = `https://localhost:7048/api/Courseoverview`;
  formData: CourseOverviewModel = new CourseOverviewModel;
  list: CourseOverviewModel[] | undefined;

  postCursussen() {
    return this.http.post(this.baseURL, this.formData)
  }

  refreshCursusLijst() {
    this.http.get(this.baseURL)
    .subscribe(res => this.list = res as CourseOverviewModel[]);
  }
}
