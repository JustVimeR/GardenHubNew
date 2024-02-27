import { WorkType } from "./WorkType";
import { Project } from "./Project";
import { Feedback } from "./Feedback";

export interface Gardener {
    descriptionOfExperience: string;
    cities: string[];
    workTypes: WorkType[];
    projects: Project[];
    feedbacks: Feedback[];
}