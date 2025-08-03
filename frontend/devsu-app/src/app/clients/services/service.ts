import { Injectable } from "@angular/core";
import { CreateClientRequest } from "../models/create.request";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { ClientResponse } from "../models/response";
import { UpdateClientRequest } from "../models/update.client.request";

@Injectable()
export class ClientService {
  private readonly api = `${environment.apiUrl}/clients`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<ClientResponse[]> {
    return this.http.get<ClientResponse[]>(this.api);
  }

  searchByName(searchTerm: string) {
    return this.http.get<ClientResponse[]>(`${this.api}?searchByName=${searchTerm}`);
  }

  getById(id: string): Observable<ClientResponse> {
    return this.http.get<ClientResponse>(`${this.api}/${id}`);
  }

  create(client: CreateClientRequest): Observable<string> {
    return this.http.post<string>(this.api, client);
  }

  update(id: string, client: UpdateClientRequest): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, client);
  }

  partialUpdate(id: string, client: Partial<UpdateClientRequest>): Observable<void> {
    return this.http.patch<void>(`${this.api}/${id}`, client);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
