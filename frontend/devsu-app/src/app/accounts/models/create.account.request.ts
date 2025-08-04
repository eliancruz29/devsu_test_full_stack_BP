export interface CreateAccountRequest {
  clientId: string;
  accountNumber?: string;
  type?: number;
  openingBalance?: number;
}
