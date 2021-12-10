import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseOverviewComponent } from './Components/course-overview/course-overview.component';
import {FileUploadComponent} from './Components/file-upload/file-upload.component';

const routes: Routes = [
  { path: '', redirectTo: '/courseoverview', pathMatch: 'full' },
  { path: 'courseoverview', component: CourseOverviewComponent },
  { path: 'uploadfile', component: FileUploadComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 
}