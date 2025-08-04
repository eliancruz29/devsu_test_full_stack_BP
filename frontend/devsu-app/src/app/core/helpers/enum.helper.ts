import { EnumObject } from "../../core/enums/enum-object";
import { Gender } from "../../core/enums/gender";
import { AccountType } from "../enums/account-type";
import { TransferType } from "../enums/transfer-type";

export function getGenderAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 0, label: Gender.Male },
    { value: 1, label: Gender.Female },
    { value: 2, label: Gender.NotSpecified }
  ];
}

export function getAccountTypeAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 0, label: AccountType.Savings },
    { value: 1, label: AccountType.Checks }
  ];
}

export function getTransferTypeAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 0, label: TransferType.Credit },
    { value: 1, label: TransferType.Debit }
  ];
}