import { CreateAccountRequest } from "./create.account.request";

export interface UpdateAccountRequest extends CreateAccountRequest{
  id: string; // UUID like '3fa85f64-5717-4562-b3fc-2c963f66afa6'
}
