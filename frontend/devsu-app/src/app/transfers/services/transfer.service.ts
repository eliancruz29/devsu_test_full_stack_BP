import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { TransferResponse } from "../models/transfer.response";
import { isNullOrUndefinedOrEmpty } from "../../core/helpers/string.helpers";
import { CreateTransferRequest } from "../models/create.transfer.request";
import { UpdateTransferRequest } from "../models/update.transfer.request";

@Injectable()
export class TransferService {
  private readonly api = `${environment.apiUrl}/transfers`;

  constructor(private http: HttpClient) { }

  getAll(accountId?: string): Observable<TransferResponse[]> {
    return this.http.get<TransferResponse[]>(this.api + (isNullOrUndefinedOrEmpty(accountId) ? '' : `?accountId=${accountId}`));
  }

  getById(id: string): Observable<TransferResponse> {
    return this.http.get<TransferResponse>(`${this.api}/${id}`);
  }

  create(transfer: CreateTransferRequest): Observable<string> {
    return this.http.post<string>(this.api, transfer);
  }

  update(id: string, transfer: UpdateTransferRequest): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, transfer);
  }

  partialUpdate(id: string, transfer: Partial<UpdateTransferRequest>): Observable<void> {
    return this.http.patch<void>(`${this.api}/${id}`, transfer);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
