import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FileUploadMessage } from '../../Models/file-upload.model';

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent{

  constructor(private http: HttpClient) { }
  fileUploadMessage: FileUploadMessage = new FileUploadMessage();
  showMessage = false;
  baseURL = 'https://localhost:7048/api/CourseImport';

  uploadFile(file: any) {
    let filetoUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', filetoUpload, filetoUpload.name);
    this.http.post(this.baseURL, formData).subscribe(
      (response) => {
        this.fileUploadMessage = response as FileUploadMessage;
        this.showMessage = true;
      }
    );
  }

  HideMessage() {
    this.showMessage = false;
  }
}