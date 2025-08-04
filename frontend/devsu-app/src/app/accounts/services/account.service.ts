import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { AccountResponse } from "../models/account.response";
import { isNullOrUndefinedOrEmpty } from "../../core/helpers/string.helpers";
import { CreateAccountRequest } from "../models/create.account.request";
import { UpdateAccountRequest } from "../models/update.account.request";

@Injectable()
export class AccountService {
  private readonly api = `${environment.apiUrl}/accounts`;

  constructor(private http: HttpClient) { }

  getAll(clientId?: string): Observable<AccountResponse[]> {
    return this.http.get<AccountResponse[]>(this.api + (isNullOrUndefinedOrEmpty(clientId) ? '' : `?clientId=${clientId}`));
  }

  getById(id: string): Observable<AccountResponse> {
    return this.http.get<AccountResponse>(`${this.api}/${id}`);
  }

  create(account: CreateAccountRequest): Observable<string> {
    return this.http.post<string>(this.api, account);
  }

  update(id: string, account: UpdateAccountRequest): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, account);
  }

  partialUpdate(id: string, account: Partial<UpdateAccountRequest>): Observable<void> {
    return this.http.patch<void>(`${this.api}/${id}`, account);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
