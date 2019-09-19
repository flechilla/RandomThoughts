import { TestBed } from '@angular/core/testing';

import { ThoughtHoleService } from './thought-hole.service';

describe('ThoughtHoleService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ThoughtHoleService = TestBed.get(ThoughtHoleService);
    expect(service).toBeTruthy();
  });
});
