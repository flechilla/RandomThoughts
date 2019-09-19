import { Base } from "./base";
import { Visibility } from './visibility.enum';

export class ThoughtHole implements Base {
  id: number;

  createAtHumanized?: string;
  modifiedAtHumanized?: string;
  likes: number;
  views: number;
  amountOfThought: number;
  name: string;
  description: string;
  visibility: Visibility;

  /**
   *
   */
  constructor(initData?: Partial<ThoughtHole>)  {
        Object.assign(this, initData);
  }
}
