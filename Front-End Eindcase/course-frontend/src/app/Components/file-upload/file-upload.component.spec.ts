import { TestBed } from '@angular/core/testing';
import { FileUploadMessage } from '../../Models/file-upload.model';
import { FileUploadComponent } from './file-upload.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('FileUploadComponent', () => {
  let httpclient: HttpClientTestingModule;
  let httpTestingController: HttpTestingController;
  let component: FileUploadComponent;
  let fileUploadMessage: FileUploadMessage;

// set up the testbed
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [FileUploadComponent],
      providers: [FileUploadComponent]
    }).compileComponents();
    httpclient = TestBed.inject(HttpClientTestingModule);
    httpTestingController = TestBed.inject(HttpTestingController);
    component = TestBed.inject(FileUploadComponent);
    fileUploadMessage = new FileUploadMessage();
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  // test the component
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
