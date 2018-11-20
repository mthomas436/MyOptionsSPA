import { Option } from './option';
import { Tradetype } from './tradetype';

export interface Trade {

  tradeid: number;
  userid: number;
  description: string;
  stockSymbol: string;
  dateEntered: Date;
  tradeTypeId: number;
  notes: string;
  tradetypes: Tradetype[];
  dateclosed?: string;
  options?: Option[];
}
