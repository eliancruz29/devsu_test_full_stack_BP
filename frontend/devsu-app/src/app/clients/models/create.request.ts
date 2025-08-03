export interface CreateClientRequest {
  name?: string;
  gender: number;
  dateOfBirth: string;
  identification?: string;
  address?: string;
  phoneNumber?: string;
  password?: string;
}
