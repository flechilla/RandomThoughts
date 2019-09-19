import { Base } from "./base";
import { Visibility } from "./visibility.enum";
import { Mood } from "./mood.enum";

export class Thought implements Base {
  id: number;
  createAtHumanized?: string;
  modifiedAtHumanized?: string;
  title: string;
  body: string;
  mood: Mood;

  visibility: Visibility;

  /**
   *
   */
  constructor(init?: Partial<Thought>) {
      Object.assign(this, init);
  }
}
