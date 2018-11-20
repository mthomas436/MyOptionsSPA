import { Optiontype } from './optiontype';

export interface Option {
  optionid: number;
  tradeid: number;
  expirationDate: Date;
  strikePrice: number;
  quantity: number;
  dateEntered: Date;
  entryPrice: number;
  optionTypeId: number;
  notes: string;
  expdate: string;
  expDateShort: string;
  positionClosed: boolean;
  optionTypeDesc: string;
  stockPriceatPurchace?: number;
  optionType?: Optiontype[];
  exitPrice?: number;
  commission?: number;
  dateClosed?: Date;
  optionCost?: number;
  closingTotalPrice?: number;
  profitLoss?: number;

}
