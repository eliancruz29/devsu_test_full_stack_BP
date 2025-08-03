import { Injectable } from "@angular/core";
import { ClientResponse, CreateClientRequest } from "../models/client.models";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable()
export class ClientService {
  private readonly api = `${environment.apiUrl}/clients`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<ClientResponse[]> {
    return this.http.get<ClientResponse[]>(this.api);
  }

  getById(id: string): Observable<ClientResponse> {
    return this.http.get<ClientResponse>(`${this.api}/${id}`);
  }

  create(client: Omit<CreateClientRequest, 'id'>): Observable<string> {
    return this.http.post<string>(this.api, client);
  }

  update(id: string, client: Partial<CreateClientRequest>): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, client);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
