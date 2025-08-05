import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransfersReportComponent } from './transfers-report.component';

describe('TransfersReportComponent', () => {
  let component: TransfersReportComponent;
  let fixture: ComponentFixture<TransfersReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransfersReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransfersReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
