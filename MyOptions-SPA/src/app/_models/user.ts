import { Trade } from './trade';

export interface User {
  userid: number;
  username: string;
  firstname: string;
  lastname: string;
  email: string;
  trades?: Trade[];
}
