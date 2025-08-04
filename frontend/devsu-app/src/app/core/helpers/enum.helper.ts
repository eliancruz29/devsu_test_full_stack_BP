import { EnumObject } from "../../core/enums/enum-object";
import { Gender } from "../../core/enums/gender";
import { AccountType } from "../enums/account-type";
import { TransferType } from "../enums/transfer-type";

export function getGenderAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 1, label: Gender.Male },
    { value: 2, label: Gender.Female },
    { value: 3, label: Gender.NotSpecified }
  ];
}

export function getAccountTypeAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 1, label: AccountType.Savings },
    { value: 2, label: AccountType.Checks }
  ];
}

export function getTransferTypeAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 1, label: TransferType.Credit },
    { value: 2, label: TransferType.Debit }
  ];
}