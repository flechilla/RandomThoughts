import { Base } from "./base";

export class Comment implements Base {
  id: number;

  createAtHumanized: string;
  modifiedAtHumanized: string;
  body: string;
  maxLength: 255;
  applicationUserId?: string;
  likes: number;

  /**
   * Get or set the value that represents the object Id
   * that is the container of this comment.
   *
   * @type {number}
   * @memberof Comment
   */
  parentId: number;


  constructor(init?: Partial<Comment>) {
    Object.assign(this, init);
  }
}
