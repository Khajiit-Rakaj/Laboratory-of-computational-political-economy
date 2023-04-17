package org.politecon.api.utils

import io.ktor.http.content.*
import kotlin.reflect.KMutableProperty
import kotlin.reflect.full.memberProperties
import kotlin.reflect.typeOf

class HTTPUtils {
    companion object {
        suspend fun getFormData(data: MultiPartData): FormData {
            val formData = FormData.create()

            data.forEachPart { partData ->
                when (partData) {
                    is PartData.FormItem -> {
                        formData.fields.add(Pair(partData.name ?: "", partData.value))
                    }

                    is PartData.FileItem -> {
                        val fileName = partData.originalFileName as String
                        val fileBytes: ByteArray = partData.streamProvider().readBytes()
                        formData.files.add(Pair(fileName, fileBytes))
                    }

                    else -> {}
                }
            }

            return formData
        }

        suspend inline fun <reified T : Any> mapFormData(data: MultiPartData): T? {
            val formData: FormData = getFormData(data)
            val model: T? = getValue()
            var fields = T::class.memberProperties
            fields.forEach { field ->
                if (field is KMutableProperty<*>) {
                    setFieldValue(field, formData, model)
                }
            }

            return model
        }

        fun <T> setFieldValue(field: KMutableProperty<*>, formData: FormData, model: T?) {
            var value: Any? = null
            if (field.returnType == typeOf<String?>() ||
                field.returnType == typeOf<Int?>()  ||
                field.returnType == typeOf<Double?>()
            ) {
                value = formData.fields.firstOrNull(predicate = { it.first == field.name })?.second
            }
            else if (field.returnType == typeOf<ArrayList<Pair<String, ByteArray>>>()){
                value = formData.files
            }

            field.setter.call(model, value)
        }

        inline fun <reified T : Any> getValue(): T? {
            val primaryConstructor = T::class.constructors.find { it.parameters.isEmpty() }
            return primaryConstructor?.call()
        }
    }
}