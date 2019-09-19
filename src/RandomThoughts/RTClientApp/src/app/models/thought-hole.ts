import { Base } from "./base";

export class ThoughtHole implements Base {
  id: number;

  createAtHumanized: string;
  modifiedAtHumanized: string;
  likes: number;
  views: number;
  amountOfThought: number;
  name: string;
  description: string;

  /**
   *
   */
  constructor(initData?: Partial<ThoughtHole>)  {
        Object.assign(this, initData);
  }
}
