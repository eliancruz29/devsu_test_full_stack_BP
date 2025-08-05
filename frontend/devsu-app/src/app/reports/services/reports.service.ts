import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { TransfersReportResponse } from "../models/transfers-report.response";

@Injectable()
export class ReportsService {
  private readonly api = `${environment.apiUrl}/reports/transfers`;

  constructor(private http: HttpClient) {}

  getTransfersReport(clientId: string, startDate: string, endDate: string): Observable<TransfersReportResponse[]> {
    const params = new HttpParams()
      .set('ClientId', clientId)
      .set('StartDate', startDate)
      .set('EndDate', endDate);

    return this.http.get<TransfersReportResponse[]>(this.api, { params });
  }
}
