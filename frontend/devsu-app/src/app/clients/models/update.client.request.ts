import { CreateClientRequest } from "./create.request";

export interface UpdateClientRequest extends CreateClientRequest{
  id: string; // UUID like '3fa85f64-5717-4562-b3fc-2c963f66afa6'
}
