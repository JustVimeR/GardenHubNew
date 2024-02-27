import { WorkType } from "./WorkType";
import { Media } from "./Media";

export interface Project {
    title: string;
    location: string;
    budget: number;
    description: string;
    numberOfRequriedGardeners: number;
    isCompleted: boolean;
    isVerified: boolean;
    publicationDate: string;
    startDate: string;
    endDate: string;
    workTypes: WorkType[];
    medias: Media[];
  }