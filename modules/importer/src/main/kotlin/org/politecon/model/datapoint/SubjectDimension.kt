package org.politecon.model.datapoint

import kotlinx.serialization.Serializable
import org.politecon.model.DataSubject
import org.politecon.model.DataDimension

@Serializable
data class SubjectDimension(val subject: DataSubject, val dimension: DataDimension)