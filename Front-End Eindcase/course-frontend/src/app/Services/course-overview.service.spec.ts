import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { CourseOverviewService } from './course-overview.service';
import { CourseOverviewModel } from '../Models/course-overview.model';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

// cursus service test
describe('CursusService', () => {
    let httpClient: HttpClientTestingModule;
    let httpTestingController: HttpTestingController;
    let service: CourseOverviewService;
    let cursus: CourseOverviewModel;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [CourseOverviewService, CourseOverviewModel]
        });
        httpClient = TestBed.inject(HttpClientTestingModule);
        httpTestingController = TestBed.inject(HttpTestingController);
        service = TestBed.inject(CourseOverviewService);
        cursus = TestBed.inject(CourseOverviewModel);
    });
    
    afterEach(() => {
        httpTestingController.verify();
    });

    it('should return an observable of courseoverview', () => {
        const dummyCursus = [
            {
                title: 'C#',
                duration: 1,
                startDate: "2018-01-01",
            }
        ];
        service.postCursussen().subscribe(
            (data: any) => {
                expect(data.length).toBe(1);
                expect(data).toEqual(dummyCursus);
            }
        );
        const req = httpTestingController.expectOne(service.baseURL);
        expect(req.request.method).toEqual('POST');
        req.flush(dummyCursus);
    });
});