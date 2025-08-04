import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { AccountResponse } from "../models/account.response";
import { isNullOrUndefinedOrEmpty } from "../../core/helpers/string.helpers";

@Injectable()
export class AccountService {
  private readonly api = `${environment.apiUrl}/accounts`;

  constructor(private http: HttpClient) { }

  getAll(clientId?: string): Observable<AccountResponse[]> {
    return this.http.get<AccountResponse[]>(this.api + (isNullOrUndefinedOrEmpty(clientId) ? '' : `?clientId=${clientId}`));
  }

  searchByName(searchTerm: string) {
    return this.http.get<AccountResponse[]>(`${this.api}?searchByName=${searchTerm}`);
  }

  getById(id: string): Observable<AccountResponse> {
    return this.http.get<AccountResponse>(`${this.api}/${id}`);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
